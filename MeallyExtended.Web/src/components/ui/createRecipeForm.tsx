import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogTrigger,
} from "@/components/ui/dialog";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { Button } from "./button";
import { z } from "zod";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm, useFieldArray, Controller } from "react-hook-form";
import { useState, useEffect } from "react";
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
import { Textarea } from "./textarea";
import { Category, Units } from "@/models/models";

interface CreateRecipeFormProps {
  setIsLoggedIn: (isLoggedIn: boolean) => void;
  setUserInfo: (userInfo: string) => void;
  setIsOpen: (isOpen: boolean) => void;
}

type FormValues = {
  title: string;
  description: string;
  imageUrl: string;
  ingredients: {
    quantity: number;
    unit: number;
    name: string;
  }[];
  instructions: string;
  duration: number;
  categories: {
    category: string;
  }[];
};

const CreateRecipeForm: React.FC<CreateRecipeFormProps> = (props) => {
  const form = useForm<FormValues>({
    defaultValues: {
      title: "",
      description: "",
      imageUrl: "",
      ingredients: [
        {
          quantity: 0,
          unit: 0,
          name: "",
        },
      ],
      instructions: "",
      duration: 0,
      categories: [{ category: "" }],
    },
  });

  const { register, control, formState } = form;
  const { errors } = formState;

  const { toast } = useToast();

  const {
    fields: categoryFields,
    append: appendCategory,
    remove: removeCategory,
  } = useFieldArray({
    name: "categories",
    control,
  });

  const {
    fields: ingredientFields,
    append: appendIngredient,
    remove: removeIngredient,
  } = useFieldArray({
    name: "ingredients",
    control,
  });

  const [categoriesList, setCategoriesList] = useState<string[]>([]);

  useEffect(() => {
    const fetchCategories = async () => {
      try {
        const response = await axios.get(
          `${apiBaseUrl}/api/Category/category-names`
        );
        setCategoriesList(response.data);

        console.log("CategoriesResponse:", response.data);
        console.log("CategoriesList:", categoriesList);
      } catch (error) {
        console.error("Error fetching categories:", error);
      }
    };

    fetchCategories();
  }, []);

  console.log(categoriesList); // Check if categoriesList is updated

  const onSubmit = form.handleSubmit((data) => {
    const categoryNames = data.categories.map((category) => category.category);

    axios
      .post(
        `${apiBaseUrl}/api/Recipe/`,
        {
          title: data.title,
          description: data.description,
          ingredients: data.ingredients,
          imageUrl: data.imageUrl,
          instructions: data.instructions,
          duration: data.duration,
          categories: categoryNames,
        },
        {
          headers: {
            "Content-Type": "application/json",
          },
          withCredentials: true,
        }
      )
      .then((response: AxiosResponse) => {
        if (response.status === 201) {
          toast({
            title: "Successully created recipe",
            description: "Sucessfully created recipe!",
            variant: "success",
          });

          props.setIsOpen(false);
        }
      })
      .catch(() => {
        setError("Failed to create recipe.");
      });
  });

  const [error, setError] = useState<string | null>(null);
  const [open, setOpen] = useState(false);

  const unitOptions = Object.entries(Units)
    .filter(([key, value]) => typeof value === "number")
    .map(([key, value]) => ({ label: key, value }));

  return (
    <div
      style={{
        position: "relative",
        width: "100%",
        maxWidth: "600px",
        margin: "0 auto",
      }}
    >
      <div style={{ overflowY: "auto", maxHeight: "80vh", padding: "0 10px" }}>
        <form
          onSubmit={onSubmit}
          style={{ display: "flex", flexDirection: "column" }}
          noValidate
        >
          <label htmlFor="title">Title</label>
          <Input
            type="text"
            id="title"
            {...register("title", { required: "Title is required" })}
          />
          {errors.title && (
            <span style={{ color: "red" }}>{errors.title.message}</span>
          )}

          <label htmlFor="description">Description</label>
          <Textarea id="description" {...register("description")} />

          <label htmlFor="imageUrl">Image URL</label>
          <Input type="text" id="imageUrl" {...register("imageUrl")} />

          <label htmlFor="instructions">Instructions</label>
          <Textarea
            id="instructions"
            {...register("instructions", {
              required: "Instructions are required",
            })}
          />
          {errors.instructions && (
            <span style={{ color: "red" }}>{errors.instructions.message}</span>
          )}

          <label htmlFor="duration">Duration in minutes</label>
          <Input
            type="number"
            id="duration"
            {...register("duration", {
              required: "Duration is required",
              validate: (value) =>
                parseInt(value) > 0 || "Duration must be a positive number",
            })}
            min={0} // Set minimum value to 0
            onChange={(e) => {
              // Convert negative values to 0
              const value = parseInt(e.target.value);
              if (value < 0) {
                e.target.value = "0";
              }
            }}
          />
          {errors.duration && (
            <span style={{ color: "red" }}>{errors.duration.message}</span>
          )}

          <div style={{ marginTop: "10px" }}>
            <label>Categories</label>
            <div style={{ display: "flex", flexDirection: "column" }}>
              {categoryFields.map((field, index) => (
                <div
                  key={field.id}
                  style={{
                    display: "flex",
                    alignItems: "center",
                    marginBottom: "10px",
                  }}
                >
                  <select
                    {...register(`categories.${index}.category` as const, {
                      required: "Category is required",
                    })}
                    style={{ flexGrow: 1, flexBasis: "0", minWidth: "0" }}
                  >
                    <option value="">Select a category</option>
                    {categoriesList.map((category, index) => (
                      <option key={index} value={category}>
                        {category}
                      </option>
                    ))}
                  </select>
                  {index > 0 && (
                    <Button
                      onClick={() => removeCategory(index)}
                      style={{ marginLeft: "10px", flexShrink: 0 }}
                    >
                      Remove
                    </Button>
                  )}
                </div>
              ))}
              {categoryFields.length < 5 && (
                <Button onClick={() => appendCategory({ category: "" })}>
                  Add category
                </Button>
              )}
            </div>
          </div>

          <div style={{ marginTop: "10px" }}>
            <label>Ingredients</label>
            <div style={{ display: "flex", flexDirection: "column" }}>
              {ingredientFields.map((field, index) => {
                return (
                  <div
                    key={field.id}
                    style={{
                      display: "flex",
                      alignItems: "center",
                      marginBottom: "10px",
                      width: "100%",
                    }}
                  >
                    <Input
                      type="text"
                      {...register(`ingredients.${index}.name` as const, {
                        required: "Ingredient name is required",
                      })}
                      style={{ flexGrow: 1, flexBasis: "0", minWidth: "0" }}
                    />

                    <Input
                      type="number"
                      defaultValue={Units.kg}
                      {...register(`ingredients.${index}.quantity` as const, {
                        required: "Quantity is required",
                      })}
                      min={0} // Set minimum value to 0
                      onChange={(e) => {
                        // Convert negative values to 0
                        const value = parseInt(e.target.value);
                        if (value < 0) {
                          e.target.value = "0";
                        }
                      }}
                      style={{ width: "100px", marginLeft: "10px" }}
                    />

                    {/* <Input
                      type="number"
                      {...register(`ingredients.${index}.unit` as const, {
                        required: "Unit of meassure is required",
                        setValueAs: (value) => parseInt(value),
                      })}
                      min={0} // Set minimum value to 0
                      onChange={(e) => {
                        // Convert negative values to 0
                        const value = parseInt(e.target.value);
                        if (value < 0) {
                          e.target.value = "0";
                        }
                      }}
                      style={{ width: "100px", marginLeft: "10px" }}
                    /> */}
                    <div style={{ margin: "10px" }}>
                      <Controller
                        name={`ingredients.${index}.unit`}
                        control={control}
                        defaultValue={Units.kg}
                        rules={{ required: "Unit of measure is required" }}
                        render={({ field }) => (
                          <select
                            {...field}
                            onChange={(e) =>
                              field.onChange(parseInt(e.target.value))
                            }
                          >
                            {unitOptions.map((unit) => (
                              <option key={unit.value} value={unit.value}>
                                {unit.value !== 7 ? unit.label : "to taste"}
                              </option>
                            ))}
                          </select>
                        )}
                      />
                    </div>
                    {index > 0 && (
                      <Button
                        onClick={() => {
                          removeIngredient(index);
                        }}
                        style={{ marginLeft: "10px", flexShrink: 0 }}
                      >
                        Remove
                      </Button>
                    )}
                  </div>
                );
              })}
              <Button
                onClick={() => {
                  appendIngredient({ name: "", quantity: 0, unit: 0 });
                }}
              >
                Add ingredient
              </Button>
            </div>
          </div>

          <Button
            type="submit"
            className="mt-4 bg-primary-background hover:bg-primary-background/70"
          >
            Submit
          </Button>
        </form>
      </div>
    </div>
  );
};
export default CreateRecipeForm;
