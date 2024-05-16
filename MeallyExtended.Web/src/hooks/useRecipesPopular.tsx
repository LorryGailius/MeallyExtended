import { useState } from 'react';
import apiBaseUrl from '../../API/apiConfig.ts';

export function useRecipesPopular() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [popularRecipes, setPopularRecipes] = useState<Array<Recipe>>([]);

  const fetchPopularRecipes = async (amount: number) => {
    setLoading(true);
    setError(null);

    try {
      const response = await fetch(`${apiBaseUrl}/api/Recipe/popular?amount=${amount}`, {
        method: 'GET',
        headers: {
          'Accept': '*/*',
        },
      });

      if (!response.ok) {
        throw new Error('Request failed');
      }

      const data = await response.json();
        console.log(data);

      setPopularRecipes(data);
    } catch (err: any) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return { fetchPopularRecipes, loading, error, popularRecipes };
}