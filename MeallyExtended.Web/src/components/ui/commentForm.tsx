import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { z } from "zod";
import { useToast } from "./use-toast";
import {
  Form,
  FormControl,
  FormField,
  FormItem,
  FormMessage,
} from "@/components/ui/form";
import { Button } from "./button";
import {
  Dialog,
  DialogContent,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import { Input } from "./input";
import { Plus } from "lucide-react";
import { useState } from "react";
import axios, { AxiosResponse } from "axios";
import apiBaseUrl from "../../../API/apiConfig";
import { Review } from "@/models/models";
import { useNavigate } from "react-router-dom";

interface CommentFormProps {
  recipeId: string;
  attachComment: () => void;
}

const commentSchema = z.object({
  comment: z.string().min(5),
});

const CommentForm: React.FC<CommentFormProps> = (props) => {
  const form = useForm<z.infer<typeof commentSchema>>({
    resolver: zodResolver(commentSchema),
    defaultValues: {
      comment: "",
    },
  });

  const { toast } = useToast();
  const nav = useNavigate();
  const [open, setOpen] = useState<boolean>(false);

  const onSubmit = form.handleSubmit((data) => {
    console.log(data);
    axios
      .post(
        `${apiBaseUrl}/api/Review`,
        {
          recipeId: props.recipeId,
          text: data.comment,
        },
        {
          withCredentials: true,
        }
      )
      .then((review: AxiosResponse<Review>) => {
        toast({
          title: "Comment added",
          description: "Your comment has been added successfully.",
          variant: "success",
        });
        form.reset();
        props.attachComment();
      })
      .catch(() => {
        toast({
          title: "Error",
          description: "An error occurred while adding your comment.",
          variant: "destructive",
        });
      }).finally(() => {
        setOpen(false);
      });
  });

  return (
    <>
      <Dialog open={open} onOpenChange={setOpen}>
        <DialogTrigger asChild>
          <Button variant="ghost">
            <Plus />
          </Button>
        </DialogTrigger>
        <DialogContent>
          <DialogTitle>Leave a comment</DialogTitle>
          <Form {...form}>
            <form onSubmit={onSubmit}>
              <FormField
                control={form.control}
                name="comment"
                render={({ field }) => (
                  <FormItem>
                    <FormControl className="relative">
                      <>
                        <Input {...field} type="text" />
                      </>
                    </FormControl>
                    <FormMessage />
                  </FormItem>
                )}
              />
              <FormItem>
                <Button type="submit" className="my-4">
                  Submit
                </Button>
              </FormItem>
            </form>
          </Form>
        </DialogContent>
      </Dialog>
    </>
  );
};

export default CommentForm;
