import { useState } from "react";

export function useSubmitRecipe() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [response, setResponse] = useState<any>(null);

  const submitRecipe = async (recipe: {
    title: string;
    description: string;
    ingredients: { quantity: number; unit: number; name: string }[];
    instructions: string;
    duration: number;
    categories: string[];
  }) => {
    setLoading(true);
    setError(null);

    try {
      const res = await fetch("https://localhost:7279/api/Recipe", {
        method: "POST",
        headers: {
          accept: "*/*",
          "Content-Type": "application/json",
        },
        body: JSON.stringify(recipe),
      });

      if (!res.ok) {
        const errorText = await res.text();
        throw new Error(`Request failed: ${res.status} - ${errorText}`);
      }

      const text = await res.text();
      const data = text ? JSON.parse(text) : {};

      setResponse(data);
    } catch (err: any) {
      console.error("Error details:", err);
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return { submitRecipe, loading, error, response };
}
