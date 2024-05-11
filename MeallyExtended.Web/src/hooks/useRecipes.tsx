import { useState, useEffect } from 'react';
import apiBaseUrl from '../../API/apiConfig.ts';

export function useSearchRecipes() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [recipes, setRecipes] = useState<Array<Recipe>>([]);

  const searchRecipes = async (query:string, categories:string, pageNo = 1, pageSize = 10) => {
    setLoading(true);
    setError(null);

    try {
    const response = await fetch(`${apiBaseUrl}/api/Recipe/search?query=${query}&categories=${categories}&pageNo=${pageNo}&pageSize=${pageSize}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
    });

    if (!response.ok) {
        throw new Error('Request failed');
    }

    const data = await response.json();
    console.log(data);

    setRecipes(data);
    } catch (err: any) {
        setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return { searchRecipes, loading, error, recipes };
}