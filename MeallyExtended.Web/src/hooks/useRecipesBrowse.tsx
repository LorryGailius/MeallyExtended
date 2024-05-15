import { useState } from 'react';
import apiBaseUrl from '../../API/apiConfig.ts';

export function useRecipesBrowse() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);
  const [recipes, setRecipes] = useState<Array<Recipe>>([]);
  const [totalPages, setTotalPages] = useState(0);

  const browseRecipes = async (pageNo: number, pageSize: number) => {
    setLoading(true);
    setError(null);

    try {
      const response = await fetch(`${apiBaseUrl}/api/Recipe/browse?pageNo=${pageNo}&pageSize=${pageSize}`, {
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

      setRecipes(data.data);
      setTotalPages(data.totalPages);
    } catch (err: any) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return { browseRecipes, loading, error, recipes, totalPages};
}