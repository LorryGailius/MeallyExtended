import React, { useState } from "react";
import { useLikeRecipe } from "@/hooks/useLikeRecipe";

function LikeRecipeComponent() {
  const { likeRecipe, loading, error, response } = useLikeRecipe();
  const [recipeId, setRecipeId] = useState("");

  const handleLike = async () => {
    // Ensure the user is authenticated before attempting to like
    const token = localStorage.getItem("authToken");
    if (!token) {
      alert("You must be logged in to like a recipe");
      return;
    }

    await likeRecipe(recipeId);
  };

  return (
    <div>
      <h2>Like a Recipe</h2>
      <input
        type="text"
        value={recipeId}
        onChange={(e) => setRecipeId(e.target.value)}
        placeholder="Enter Recipe ID"
        required
      />
      <button onClick={handleLike} disabled={loading}>
        Like Recipe
      </button>
      {loading && <p>Loading...</p>}
      {error && <p>Error: {error}</p>}
      {response && <p>Success: {JSON.stringify(response)}</p>}
    </div>
  );
}

export default LikeRecipeComponent;
