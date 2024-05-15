import { useState } from 'react';
import apiBaseUrl from '../../API/apiConfig.ts';

export function useAuthenticate() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState(null);

  const authenticateUser = async (userEmail: string, userPassword: string) => {
    setLoading(true);
    setError(null);

    try {
      const response = await fetch(`${apiBaseUrl}/login?useCookies=true&useSessionCookies=true`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify({ email: userEmail, password: userPassword })
      });

      console.log(response);

      if (!response.ok) {
        throw new Error('Request failed');
      }

      const text = await response.text();
      const data = text ? JSON.parse(text) : {};

      if (!data) {
        throw new Error('No data returned from server');
      }

    } catch (err: any) {
      console.error(err);
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return { authenticateUser, loading, error };
}

export default useAuthenticate;