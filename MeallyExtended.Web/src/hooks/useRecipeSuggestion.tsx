import { useState } from "react";

export function useRecipeSuggestion() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [response, setResponse] = useState<any>(null);

  const getSuggestions = async (query: string, amount: number) => {
    setLoading(true);
    setError(null);

    try {
      const response = await fetch(
        `https://localhost:7279/api/Recipe/suggestion?query=${query}&amount=${amount}`,
        {
          method: "GET",
          headers: {
            accept: "*/*",
          },
        }
      );

      if (!response.ok) {
        const errorText = await response.text();
        throw new Error(`Request failed: ${response.status} - ${errorText}`);
      }

      const data = await response.json();

      setResponse(data);
    } catch (err: any) {
      console.error("Error details:", err);
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return { getSuggestions, loading, error, response };
}
