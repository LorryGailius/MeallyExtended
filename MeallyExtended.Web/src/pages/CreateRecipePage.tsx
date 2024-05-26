import Header from "@/components/ui/header";
import axios, { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import apiBaseUrl from "../../API/apiConfig";
// import { useParams } from "react-router";
import { Ingredient, RecipeViewModel, Review, Units } from "@/models/models";
import { Button } from "@/components/ui/button";
import { Heart, Pencil } from "lucide-react";
import { useToast } from "@/components/ui/use-toast";
import ReviewComponent from "@/components/ui/comment";
import { NavigateFunction, useNavigate } from "react-router-dom";

import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";

import CreateRecipeForm from "@/components/ui/createRecipeForm";

const CreateRecipePage: React.FC = () => {
  //   const { recipe_id } = useParams<string>();
  const [commentsLoading, setCommentsLoading] = useState<boolean>(true);
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  const [recipe, setRecipe] = useState<RecipeViewModel | null>(null);
  const [recipeReviews, setRecipeReviews] = useState<Review[]>([]);
  const [showMore, setShowMore] = useState<boolean>(true);
  const [userInfo, setUserInfo] = useState<string>("");
  const [open, setOpen] = useState<boolean>(false);
  const nav: NavigateFunction = useNavigate();
  const { toast } = useToast();

  useEffect(() => {}, [recipe]);

  return (
    <div id="recipe-page" className="h-screen pb-10">
      <Header setIsLoggedIn={setIsLoggedIn} setUserInfo={setUserInfo} />

      <div className="my-4">
        <div className="w-full bg-primary-background p-10 flex justify-between">
          <div className="flex justify-center gap-24"></div>

          {isLoggedIn && userInfo === recipe?.userEmail && (
            <Button className="bg-transparent">
              <Pencil />
            </Button>
          )}
        </div>
      </div>
      <div className="flex justify-between"></div>

      {/* Dialog for Creating Recipe */}
      <Dialog open={open} onOpenChange={setOpen}>
        <DialogTrigger asChild>
          <Button>Create New Recipe</Button>
        </DialogTrigger>
        <DialogContent>
          <DialogHeader>
            <DialogTitle>Create New Recipe</DialogTitle>
          </DialogHeader>
          <CreateRecipeForm
            setIsLoggedIn={setIsLoggedIn}
            setUserInfo={setUserInfo}
          />
        </DialogContent>
      </Dialog>
    </div>
  );
};

export default CreateRecipePage;
