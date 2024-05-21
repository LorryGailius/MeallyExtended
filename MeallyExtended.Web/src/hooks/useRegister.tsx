import { useState } from "react";

export function useRegister() {
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [response, setResponse] = useState<any>(null);

  const register = async (email: string, password: string) => {
    setLoading(true);
    setError(null);

    try {
      const response = await fetch("https://localhost:7279/register", {
        method: "POST",
        headers: {
          accept: "*/*",
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password }),
      });

      if (!response.ok) {
        throw new Error("Request failed");
      }

      const text = await response.text();
      const data = text ? JSON.parse(text) : {};

      setResponse(data);
    } catch (err: any) {
      console.error(err);
      setError(err.message);
    } finally {
      setLoading(false);
    }
  };

  return { register, loading, error, response };
}
