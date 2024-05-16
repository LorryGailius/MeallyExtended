import { useState } from 'react';
import apiBaseUrl from '../../API/apiConfig.ts';

export function useRecipe() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [recipe, setRecipe] = useState<Recipe>();

  const getRecipe = async (recipe_id: string) => {
  setLoading(true);
  setError(null);

  try {
    const response = await fetch(`${apiBaseUrl}/api/Recipe/${recipe_id}:guid`, {
        method: 'GET',
        });
    
        console.log(response);

    if (!response.ok) {
      throw new Error('Request failed');
    }

    const data = await response.json();

    if (!data) {
      throw new Error('No data returned from server');
    }

    setRecipe(data);
  } catch (err: any) {
    console.error(err);
    setError(err.message);
  } finally {
    setLoading(false);
  }
};

  return { getRecipe, loading, error, recipe };
}