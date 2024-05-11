import { Button } from "@/components/ui/button";
import { useEffect, useState } from "react";
import { useSearchRecipes } from "@/hooks/useRecipes";
import {
  Pagination,
  PaginationContent,
  PaginationEllipsis,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/ui/pagination";

const IndexPage: React.FC = () => {
  //const { searchRecipes: searchPopularRecipes, loading: loadingPopular, error: errorPopular, recipes: _popularRecipes} = useSearchRecipes();
  const { searchRecipes, loading, error, recipes} = useSearchRecipes();
  const [query, setQuery] = useState('');
  const [categories, setCategories] = useState('');
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [popularRecipes, setPopularRecipes] = useState<Array<Recipe>>([]);
  const [searchResults, setSearchResults] = useState<Recipe[]>([]);

  // const handleSubmit = async (event: { preventDefault: () => void; }) => {
  //   event.preventDefault();
  //   await search(query, categories, page, pageSize);
  //   console.log(_searchResults); // Log _searchResults
  //   setSearchResults(_searchResults);
  // };

  useEffect(() => {
      searchRecipes('Pasta', 'pasta');
    }, []);

  useEffect(() => {
    setPopularRecipes(Array.from(recipes));
    console.log(popularRecipes);
  },[!loading && !error && recipes]);

  if (loading) {
    return <div>Loading popular recipes...</div>;
  }

  if (error) {
    return <div>Error: {error}</div>;
  }

  return (
    <>
      <div id="index-page" className="h-screen">
        <div className="flex flex-col gap-4 justify-center" style={{padding: 40}}>
          <h1 className="text-4xl font-bold">Meally</h1>
          <p className="text-lg">Your meal planner</p>
        </div>
        <div className="flex flex-col gap-4 justify-cente bg-primary-background items-center" style={{padding: 40}}>
            <h2 className="text-3xl font-regular text-white">Popular recipes today</h2>
            <div className="grid grid-cols-5 gap-4">
              {loading && <div>Loading...</div>}
                {error && <div>Error: {error}</div>}
                {recipes && (
                  popularRecipes.map((recipe) => (
                    <div key={recipe.id} className="flex flex-col items-center">
                      <Button>
                        {/* <img src={recipe.image} alt={recipe.title} /> */}
                      </Button>
                      <p className="text-sm">{recipe.duration} minutes</p>
                      <p className="text-sm">{recipe.categories[0].Name}</p>
                      <p className="text-lg">{recipe.title}</p>
                    </div>
                  ))
                )}
        </div>
        </div>
        <div className="flex flex-col gap-4 justify-center" style={{padding: 40}}>
          <div>
            <form>
              <input type="text" value={query} onChange={(e) => setQuery(e.target.value)} placeholder="Search query" />
              <input type="text" value={categories} onChange={(e) => setCategories(e.target.value)} placeholder="Categories" />
              <button type="submit" disabled={loading}>Search Recipes</button>
            </form>
          </div>
          <div className="grid grid-cols-5 gap-4">
            {searchResults.map((recipe) => (
              <div key={recipe.id} className="flex flex-col items-center">
                <Button>
                  {/* <img src={recipe.image} alt={recipe.title} /> */}
                </Button>
                <p className="text-sm">{recipe.duration} minutes</p>
                <p className="text-sm">{recipe.categories[0].Name}</p>
                <p className="text-lg">{recipe.title}</p>
              </div>
            ))}
          </div>
        </div>
        <Pagination>
      <PaginationContent>
        <PaginationItem>
          <PaginationPrevious href="#" />
        </PaginationItem>
        <PaginationItem>
          <PaginationLink href="#">1</PaginationLink>
        </PaginationItem>
        <PaginationItem>
          <PaginationLink href="#" isActive>
            2
          </PaginationLink>
        </PaginationItem>
        <PaginationItem>
          <PaginationLink href="#">3</PaginationLink>
        </PaginationItem>
        <PaginationItem>
          <PaginationEllipsis />
        </PaginationItem>
        <PaginationItem>
          <PaginationNext href="#" />
        </PaginationItem>
      </PaginationContent>
    </Pagination>
      </div>
    </>
  );
};

export default IndexPage;