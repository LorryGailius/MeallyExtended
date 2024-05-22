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
import axios, { AxiosError, AxiosResponse } from "axios";
import { DialogDescription } from "@radix-ui/react-dialog";
import { useToast } from "./use-toast";
import { EyeIcon, EyeOff } from "lucide-react";

const loginSchema = z.object({
  email: z.string().email(),
  password: z.string(),
});

const RegisterForm: React.FC = () => {
  const [error, setError] = useState<string | null>(null);
  const [showPassword, setShowPassword] = useState<boolean>(false);

  const form = useForm<z.infer<typeof loginSchema>>({
    resolver: zodResolver(loginSchema),
    defaultValues: {
      email: "",
      password: "",
    },
  });

  const { toast } = useToast();

  const onSubmit = form.handleSubmit((data) => {
    axios
      .post(`${apiBaseUrl}/register`, data, {
        headers: {
          "Content-Type": "application/json",
        },
        withCredentials: true,
      })
      .then((response: AxiosResponse) => {
        if (response.status === 200) {
          window.location.reload();
          toast({
            title: "Registration Successful",
            description: "You have successfully joined Meally!",
            variant: "success",
          });
        }
      })
      .catch((error: AxiosError) => {
        if (error.response?.status === 400) {
          setError(
            Object.values((error.response?.data as any).errors).join(" ")
          );
        } else {
          toast({
            title: "Registration Failed",
            description: "Please try again later",
            variant: "destructive",
          });
        }
      });
  });

  return (
    <Dialog>
      <DialogTrigger asChild>
        <Button variant="secondary">Register</Button>
      </DialogTrigger>
      <DialogContent>
        <DialogHeader>
          <DialogTitle className="text-4xl">Join Meally!</DialogTitle>
          <DialogDescription className="text-s">
            We are excited to have you join our community! <br />
            Once you register, you can start creating your own recipes, planning
            your meals and communicating with our community.
          </DialogDescription>
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
                      <>
                      <Input
                        type={showPassword ? "text" : "password"}
                        {...field}
                        className="pr-10" // make room for the icon
                      />
                      <button
                        type="button"
                        onClick={() => setShowPassword(!showPassword)}
                        className="absolute right-10 bottom-[5.5rem]"
                      >
                        {showPassword ? <EyeIcon /> : <EyeOff />}
                      </button>
                      </>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormMessage>{error}</FormMessage>
              <Button type="submit" className="mt-4" variant="secondary">
                Register
              </Button>
            </form>
          </Form>
        </div>
      </DialogContent>
    </Dialog>
  );
};

export default RegisterForm;
