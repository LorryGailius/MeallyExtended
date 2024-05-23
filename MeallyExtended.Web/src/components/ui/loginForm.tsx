import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Button } from "./button";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { useState } from "react";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormLabel,
  FormMessage,
} from "@/components/ui/form";
import { Input } from "./input";
import apiBaseUrl from "../../../API/apiConfig";
import { Checkbox } from "./checkbox";
import axios, { AxiosResponse } from "axios";
import { useToast } from "@/components/ui/use-toast";
import { EyeIcon, EyeOff } from "lucide-react";

interface LoginFormProps {
  setIsLoggedIn: (isLoggedIn: boolean) => void;
  setUserInfo: (userInfo: string) => void;
}

const loginSchema = z.object({
  email: z.string().email(),
  password: z.string().min(8),
  rememberMe: z.boolean().optional(),
});

const LoginForm: React.FC<LoginFormProps> = (props) => {
  const form = useForm<z.infer<typeof loginSchema>>({
    resolver: zodResolver(loginSchema),
    defaultValues: {
      email: "",
      password: "",
      rememberMe: false,
    },
  });

  const [showPassword, setShowPassword] = useState<boolean>(false);
  const { toast } = useToast();

  const onSubmit = form.handleSubmit((data) => {
    axios
      .post(
        `${apiBaseUrl}/login?useCookies=true&useSessionCookies=${!data.rememberMe}`,
        {
          email: data.email,
          password: data.password,
        },
        {
          headers: {
            "Content-Type": "application/json",
          },
          withCredentials: true,
        }
      )
      .then((response: AxiosResponse) => {
        if (response.status === 200) {
          props.setIsLoggedIn(true);
          props.setUserInfo(data.email);
          toast({
            title: "Login successful",
            description: "You are now logged in",
            variant: "success",
          });

          setOpen(false);
        }
      })
      .catch(() => {
        setError("Invalid email or password");
      });
  });

  const [error, setError] = useState<string | null>(null);
  const [open, setOpen] = useState(false);
  return (
    <Dialog open={open} onOpenChange={setOpen}>
      <DialogTrigger asChild>
        <Button>Login</Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle>Welcome back!</DialogTitle>
        </DialogHeader>
        <div>
          {/* shadcnUI form */}
          <Form {...form}>
            <form onSubmit={onSubmit}>
              <FormField
                control={form.control}
                name="email"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Email</FormLabel>
                    <FormControl>
                      <Input type="email" {...field} />
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />

              <FormField
                control={form.control}
                name="password"
                render={({ field }) => (
                  <FormItem>
                    <FormLabel>Password</FormLabel>
                    <FormControl className="relative">
                      <div className="flex gap-4">
                        <Input
                          type={showPassword ? "text" : "password"}
                          {...field}
                          className="pr-10" // make room for the icon
                        />
                        <button
                          type="button"
                          onClick={() => setShowPassword(!showPassword)}
                          className=""
                        >
                          {showPassword ? <EyeIcon /> : <EyeOff />}
                        </button>
                      </div>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <div className="flex items-center my-2">
                <FormField
                  control={form.control}
                  name="rememberMe"
                  render={({ field }) => (
                    <FormItem>
                      <FormControl>
                        <Checkbox
                          className="mr-2"
                          checked={field.value}
                          onCheckedChange={field.onChange}
                        />
                      </FormControl>
                      <FormLabel>Remember me</FormLabel>
                      <FormMessage />
                    </FormItem>
                  )}
                />
              </div>
              <FormMessage>{error}</FormMessage>
              <Button
                type="submit"
                className="mt-4 bg-primary-background hover:bg-primary-background/70"
              >
                Login
              </Button>
            </form>
          </Form>
        </div>
      </DialogContent>
    </Dialog>
  );
};

export default LoginForm;
