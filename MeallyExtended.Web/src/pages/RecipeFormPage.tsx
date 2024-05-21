import React, { useState } from "react";
import { useSubmitRecipe } from "@/hooks/useSubmitRecipe";

function RecipeForm() {
  const { submitRecipe, loading, error, response } = useSubmitRecipe();
  const [title, setTitle] = useState("");
  const [description, setDescription] = useState("");
  const [ingredients, setIngredients] = useState([
    { quantity: 0, unit: 0, name: "" },
  ]);
  const [instructions, setInstructions] = useState("");
  const [duration, setDuration] = useState(0);
  const [categories, setCategories] = useState([""]);

  const handleSubmit = async (event: React.FormEvent) => {
    event.preventDefault();
    await submitRecipe({
      title,
      description,
      ingredients,
      instructions,
      duration,
      categories,
    });
  };

  const handleIngredientChange = (index: number, field: string, value: any) => {
    const newIngredients = [...ingredients];
    newIngredients[index] = { ...newIngredients[index], [field]: value };
    setIngredients(newIngredients);
  };

  const addIngredient = () => {
    setIngredients([...ingredients, { quantity: 0, unit: 0, name: "" }]);
  };

  return (
    <div>
      <h2>Recipe Page</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Title:</label>
          <input
            type="text"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
            required
          />
        </div>
        <div>
          <label>Description:</label>
          <textarea
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            required
          />
        </div>
        <div>
          <label>Ingredients:</label>
          {ingredients.map((ingredient, index) => (
            <div key={index}>
              <input
                type="number"
                placeholder="Quantity"
                value={ingredient.quantity}
                onChange={(e) =>
                  handleIngredientChange(index, "quantity", e.target.value)
                }
                required
              />
              <input
                type="number"
                placeholder="Unit"
                value={ingredient.unit}
                onChange={(e) =>
                  handleIngredientChange(index, "unit", e.target.value)
                }
                required
              />
              <input
                type="text"
                placeholder="Name"
                value={ingredient.name}
                onChange={(e) =>
                  handleIngredientChange(index, "name", e.target.value)
                }
                required
              />
            </div>
          ))}
          <button type="button" onClick={addIngredient}>
            Add Ingredient
          </button>
        </div>
        <div>
          <label>Instructions:</label>
          <textarea
            value={instructions}
            onChange={(e) => setInstructions(e.target.value)}
            required
          />
        </div>
        <div>
          <label>Duration:</label>
          <input
            type="number"
            value={duration}
            onChange={(e) => setDuration(Number(e.target.value))}
            required
          />
        </div>
        <div>
          <label>Categories:</label>
          <input
            type="text"
            value={categories}
            onChange={(e) => setCategories(e.target.value.split(","))}
            required
          />
        </div>
        <button type="submit" disabled={loading}>
          Submit Recipe
        </button>
      </form>
      {loading && <p>Loading...</p>}
      {error && <p>Error: {error}</p>}
      {response && <p>Success: {JSON.stringify(response)}</p>}
    </div>
  );
}

export default RecipeForm;
