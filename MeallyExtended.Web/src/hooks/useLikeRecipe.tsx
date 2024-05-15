import { useState } from "react";

export function useLikeRecipe() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [response, setResponse] = useState<any>(null);

  const likeRecipe = async (recipeId: string) => {
    setLoading(true);
    setError(null);

    try {
      const token = localStorage.getItem("authToken"); // Example of retrieving a token from localStorage
      const response = await fetch("https://localhost:7279/api/Recipe/like", {
        method: "POST",
        headers: {
          accept: "*/*",
          "Content-Type": "application/json",
          Authorization: `Bearer ${token}`, // Include the token in the header
        },
        body: JSON.stringify(recipeId),
      });

      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(`Request failed: ${response.status} - ${errorText}`);
      }

      const text = await response.text();
      const data = text ? JSON.parse(text) : {};

      setResponse(data);
    } catch (err: any) {
      console.error("Error details:", err);
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return { likeRecipe, loading, error, response };
}
