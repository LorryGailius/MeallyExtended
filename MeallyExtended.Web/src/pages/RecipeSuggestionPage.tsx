import React, { useState } from "react";
import { useRecipeSuggestion } from "@/hooks/useRecipeSuggestion";

function RecipeSuggestionComponent() {
  const { getSuggestions, loading, error, response } = useRecipeSuggestion();
  const [query, setQuery] = useState("");
  const [amount, setAmount] = useState(5);

  const handleSearch = async () => {
    await getSuggestions(query, amount);
  };

  return (
    <div>
      <h2>Recipe Suggestions</h2>
      <div>
        <label>Query:</label>
        <input
          type="text"
          value={query}
          onChange={(e) => setQuery(e.target.value)}
          placeholder="Enter search query"
          required
        />
      </div>
      <div>
        <label>Amount:</label>
        <input
          type="number"
          value={amount}
          onChange={(e) => setAmount(Number(e.target.value))}
          required
        />
      </div>
      <button onClick={handleSearch} disabled={loading}>
        Get Suggestions
      </button>
      {loading && <p>Loading...</p>}
      {error && <p>Error: {error}</p>}
      {response && <p>Results: {JSON.stringify(response)}</p>}
    </div>
  );
}

export default RecipeSuggestionComponent;
