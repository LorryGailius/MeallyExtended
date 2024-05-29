import { useState } from "react";

export function useSearchRecipes() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [response, setResponse] = useState<any>(null);

  const searchRecipes = async (
    query: string,
    pageNo: number,
    pageSize: number
  ) => {
    setLoading(true);
    setError(null);

    try {
      const response = await fetch(
        `https://localhost:7279/api/Recipe/search?query=${query}&pageNo=${pageNo}&pageSize=${pageSize}`,
        {
          method: "POST",
          headers: {
            accept: "text/plain",
            "Content-Type": "application/json",
          },
          body: "", // No body content needed as per the provided CURL request
        }
      );

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

  return { searchRecipes, loading, error, response };
}
