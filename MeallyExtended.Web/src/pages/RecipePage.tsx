import Header from "@/components/ui/header";
import axios, { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import apiBaseUrl from "../../API/apiConfig";
import { useParams } from "react-router";
import { Ingredient, RecipeViewModel, Review, Units } from "@/models/models";
import { Button } from "@/components/ui/button";
import { Heart, Minus, Pencil } from "lucide-react";
import { useToast } from "@/components/ui/use-toast";
import ReviewComponent from "@/components/ui/comment";
import { NavigateFunction, useNavigate } from "react-router-dom";
import placeholder from "@/assets/placeholder.png";
import CommentForm from "@/components/ui/commentForm";
import { Input } from "@/components/ui/input";

const RecipePage: React.FC = () => {
  const { recipe_id } = useParams<string>();
  const [loading, setLoading] = useState<boolean>(true);
  const [commentsLoading, setCommentsLoading] = useState<boolean>(true);
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  const [recipe, setRecipe] = useState<RecipeViewModel | null>(null);
  const [recipeReviews, setRecipeReviews] = useState<Review[]>([]);
  const [showMore, setShowMore] = useState<boolean>(true);
  const [userInfo, setUserInfo] = useState<string>("");
  const [editMode, setEditMode] = useState<boolean>(false);
  const nav: NavigateFunction = useNavigate();
  const { toast } = useToast();

  useEffect(() => {
    if (recipe_id) {
      fetchRecipe(recipe_id);
      fetchComments(recipe_id);
    }
    setLoading(false);
  }, []);

  const fetchRecipe = (recipeId: string) => {
    axios
      .get(`${apiBaseUrl}/api/Recipe/${recipeId}`, { withCredentials: true })
      .then((response: AxiosResponse<RecipeViewModel>) => {
        setRecipe(response.data);
        setLoading(false);
      })
      .catch((error) => {
        if (error.response.status === 404) {
          nav("/not-found");
        }
      });
  };

  const fetchComments = (
    recipeId: string,
    skip: number = 0,
    append: boolean = false
  ) => {
    axios
      .get(`${apiBaseUrl}/api/Review/${recipeId}?limit=5&skip=${skip}`, {
        withCredentials: true,
      })
      .then((response: AxiosResponse<Review[]>) => {
        if (append) {
          setRecipeReviews((prev) => [...prev, ...response.data]);
          if (response.data.length === 0) {
            setShowMore(false);
          }
        } else {
          setRecipeReviews(response.data);
        }
        setCommentsLoading(false);
      })
      .catch((error) => {
        console.error(error);
      });
  };

  const attachComment = () => {
    fetchComments(recipe_id!);
    setShowMore(true);
  };

  const getMoreComments = () => {
    fetchComments(recipe_id!, recipeReviews.length, true);
  };

  const enableEditMode = () => {
    setEditMode(true);
  };

  const LikeRecipe = () => {
    if (!isLoggedIn) {
      toast({
        title: "Login required",
        description: "You need to be logged in to like a recipe",
        variant: "destructive",
      });
      return;
    }

    axios
      .post(
        `${apiBaseUrl}/api/Recipe/like?recipeId=${recipe!.id}`,
        {},
        { withCredentials: true }
      )
      .then(() => {
        setRecipe((prev) => {
          if (prev) {
            return { ...prev, isFavorite: !prev.isFavorite };
          }
          return prev;
        });
      })
      .catch((error) => {
        console.error(error);
      });
  };

  const cancelEdit = () => {
    setEditMode(false);
    fetchRecipe(recipe_id!);
    fetchComments(recipe_id!);
  };

  const saveEdit = () => {
    axios
      .put(
        `${apiBaseUrl}/api/Recipe`,
        {
          id: recipe?.id,
          title: recipe?.title,
          description: recipe?.description,
          imageUrl: recipe?.imageUrl,
          ingredients: recipe?.ingredients,
          instructions: recipe?.instructions,
          duration: recipe?.duration,
          categories: recipe?.categories.map((category) => {
            return category.name;
          }),
          version: recipe?.version,
        },
        {
          withCredentials: true,
        }
      )
      .then(() => {
        setEditMode(false);
        fetchRecipe(recipe_id!);
        fetchComments(recipe_id!);
        toast({
          title: "Recipe updated",
          description: "Your recipe has been updated successfully",
          variant: "success",
        });
      })
      .catch((error) => {
        if (error.response.status === 409) {
          toast({
            title: "Conflict",
            description:
              "This recipe has been updated while editing, please refresh the page and try again.",
            variant: "destructive",
          });
        }
      });
  };

  return (
    <div id="recipe-page" className="h-screen pb-10">
      <Header setIsLoggedIn={setIsLoggedIn} setUserInfo={setUserInfo} />
      {loading && <div>Loading...</div>}
      {!loading && recipe && (
        <>
          <div className="my-4">
            <div className="w-full bg-primary-background p-10 flex justify-between">
              <div className="flex justify-center gap-24">
                {!editMode && (
                  <h1 className="text-4xl font-bold text-white">
                    {recipe?.title}
                  </h1>
                )}

                {editMode && (
                  <textarea
                    defaultValue={recipe?.title}
                    className="text-4xl font-bold text-black px-4 bg-primary-background"
                    onChange={(e) => {
                      setRecipe((prev) => {
                        if (prev) {
                          return { ...prev, title: e.target.value };
                        }
                        return prev;
                      });
                    }}
                  />
                )}

                <div>
                  <div className="flex gap-4">
                    {recipe?.categories.map((category) => (
                      <div
                        key={category.name}
                        className="bg-accent p-2 rounded-md text-white"
                      >
                        <p className="font-semibold select-none">
                          {category.name}
                        </p>
                      </div>
                    ))}
                  </div>
                </div>
              </div>

              {userInfo !== recipe?.userEmail && (
                <Button className="bg-transparent" onClick={LikeRecipe}>
                  {recipe?.isFavorite && isLoggedIn ? <Heart fill="white" /> : <Heart />}
                </Button>
              )}

              {isLoggedIn && !editMode && userInfo === recipe?.userEmail && (
                <Button className="bg-transparent" onClick={enableEditMode}>
                  <Pencil />
                </Button>
              )}

              {isLoggedIn && editMode && (
                <div className="flex gap-4">
                  <Button
                    onClick={cancelEdit}
                    variant="link"
                  >
                    Cancel
                  </Button>
                  <Button
                    onClick={saveEdit}
                    variant="secondary"
                  >
                    Save
                  </Button>
                </div>
              )}
            </div>
          </div>
          <div className="flex justify-between">
            <div className="mx-10">
              <div className="flex gap-24">
                <div className="border-8 border-white shadow-lg w-[400px] h-[400px]">
                  <div>
                    {editMode && (
                      <Input
                        type="url"
                        defaultValue={recipe?.imageUrl}
                        className="w-full h-full"
                        onChange={(e) => {
                          setRecipe((prev) => {
                            if (prev) {
                              return { ...prev, imageUrl: e.target.value };
                            }
                            return prev;
                          });
                        }}
                      />
                    )}
                    <img
                      src={recipe?.imageUrl ? recipe.imageUrl : placeholder}
                      alt={recipe?.title}
                      width={400}
                      height={400}
                    />
                  </div>
                </div>
                <div>
                  <h1 className="text-3xl font-bold mb-4">Ingredients</h1>
                  <ul className="max-h-[400px] flex flex-col flex-wrap gap-x-20">
                    {recipe?.ingredients.map((ingredient: Ingredient) => (
                      <div className="flex gap-4">
                        <li key={ingredient.name}>
                          {ingredient.quantity}{" "}
                          {ingredient.unit != 4
                            ? Units[ingredient.unit].toString()
                            : null}{" "}
                          {ingredient.name}
                        </li>
                      </div>
                    ))}
                  </ul>
                </div>
              </div>
              <div className="my-10 mx-4 max-w-6xl">
                <h1 className="text-3xl font-bold mb-4">Description</h1>
                {!editMode && <p>{recipe?.description}</p>}
                {editMode && (
                  <textarea
                    className="w-full h-64 p-4"
                    defaultValue={recipe?.description}
                    onChange={(e) => {
                      setRecipe((prev) => {
                        if (prev) {
                          return { ...prev, description: e.target.value };
                        }
                        return prev;
                      });
                    }}
                  ></textarea>
                )}
              </div>
              <div className="my-10 mx-4 max-w-6xl">
                <h1 className="text-3xl font-bold mb-4">Instructions</h1>
                {!editMode && <p>{recipe?.instructions}</p>}
                {editMode && (
                  <textarea
                    className="w-full h-64 p-4"
                    defaultValue={recipe?.instructions}
                    onChange={(e) => {
                      setRecipe((prev) => {
                        if (prev) {
                          return { ...prev, instructions: e.target.value };
                        }
                        return prev;
                      });
                    }}
                  ></textarea>
                )}
              </div>
            </div>
            <div className="max-w-xl w-full">
              <div className="flex flex-col h-full">
                <div className="flex justify-between mb-4 pr-10">
                  <h1 className="text-3xl font-bold">Comments</h1>
                  {isLoggedIn && !editMode && (
                    <CommentForm
                      recipeId={recipe_id!}
                      attachComment={attachComment}
                    />
                  )}
                </div>

                {editMode && <h1>Comments are unavailable during editing</h1>}
                {!editMode && (
                  <div className="h-full">
                    {recipeReviews.length === 0 && !commentsLoading && (
                      <div className="text-start">No comments yet</div>
                    )}

                    {commentsLoading && <div>Loading comments...</div>}
                    <div className="pr-10 max-h-[50vh]">
                      {recipeReviews.length > 0 && (
                        <div className="max-h-[64vh] flex flex-col gap-4 overflow-y-auto pr-4 scroll-smooth">
                          {recipeReviews.map((review) => (
                            <ReviewComponent key={review.id} review={review} />
                          ))}
                          {showMore && recipeReviews.length !== 0 && (
                            <div className="flex justify-center">
                              <Button variant="ghost" onClick={getMoreComments}>
                                Show more
                              </Button>
                            </div>
                          )}

                          {recipeReviews.length > 0 && !showMore && (
                            <div className="text-center">No more comments</div>
                          )}
                        </div>
                      )}
                    </div>
                  </div>
                )}
              </div>
            </div>
          </div>
        </>
      )}
    </div>
  );
};

export default RecipePage;
