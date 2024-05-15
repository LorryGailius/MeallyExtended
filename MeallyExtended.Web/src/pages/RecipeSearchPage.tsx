import React, { useState } from "react";
import { useSearchRecipes } from "@/hooks/useSearchRecipes";

function SearchRecipesComponent() {
  const { searchRecipes, loading, error, response } = useSearchRecipes();
  const [query, setQuery] = useState("");
  const [pageNo, setPageNo] = useState(1);
  const [pageSize, setPageSize] = useState(10);

  const handleSearch = async () => {
    await searchRecipes(query, pageNo, pageSize);
  };

  return (
    <div>
      <h2>Search Recipes</h2>
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
        <label>Page No:</label>
        <input
          type="number"
          value={pageNo}
          onChange={(e) => setPageNo(Number(e.target.value))}
          required
        />
      </div>
      <div>
        <label>Page Size:</label>
        <input
          type="number"
          value={pageSize}
          onChange={(e) => setPageSize(Number(e.target.value))}
          required
        />
      </div>
      <button onClick={handleSearch} disabled={loading}>
        Search Recipes
      </button>
      {loading && <p>Loading...</p>}
      {error && <p>Error: {error}</p>}
      {response && <p>Results: {JSON.stringify(response)}</p>}
    </div>
  );
}

export default SearchRecipesComponent;
