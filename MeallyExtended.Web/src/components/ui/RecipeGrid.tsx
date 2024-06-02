import React from "react";
import { Link } from "react-router-dom";
import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from "@/components/ui/pagination";
import { RecipeViewModel } from "@/models/models";
import placeholder from "@/assets/placeholder.png";

interface RecipeGridProps {
  recipes: RecipeViewModel[];
  columns: number;
  currentPage?: number;
  totalPages?: number;
  fetchPage?: (page: number) => void;
  pagination?: boolean;
}

const RecipeGrid: React.FC<RecipeGridProps> = ({
  recipes,
  columns,
  currentPage,
  totalPages,
  fetchPage,
  pagination,
}) => {
  return (
    <div>
      <div className={`grid grid-cols-5 gap-4`}>
        {recipes.map((recipe) => (
          <Link to={`/recipe/${recipe.id}`} key={recipe.id}>
            <div className="flex flex-col items-center p-4">
              <div className="w-[200px] h-[200px] overflow-hidden flex">
                <img
                  src={recipe.imageUrl || placeholder}
                  alt={recipe.title}
                  className="shrink-0 min-w-full min-h-full"
                />
              </div>
              <p className="text-sm">{recipe.duration} minutes</p>
              <p className="text-sm">
                {recipe.categories.length !== 0
                  ? recipe.categories[0].name
                  : ""}
              </p>
              <p className="text-lg">{recipe.title}</p>
            </div>
          </Link>
        ))}
      </div>
      {pagination && (totalPages ?? 0) > 1 && (
        <Pagination>
          <PaginationContent>
            <PaginationItem>
              <PaginationPrevious
                href="#"
                onClick={() =>
                  fetchPage && fetchPage(Math.max(currentPage ?? 1 - 1, 1))
                }
              />
            </PaginationItem>
            {Array.from({ length: totalPages ?? 0 }, (_, i) => (
              <PaginationItem key={i}>
                <PaginationLink
                  href="#"
                  isActive={i + 1 === currentPage}
                  onClick={() => fetchPage && fetchPage(i + 1)}
                >
                  {i + 1}
                </PaginationLink>
              </PaginationItem>
            ))}
            <PaginationItem>
              <PaginationNext
                href="#"
                onClick={() =>
                  fetchPage &&
                  fetchPage(Math.min((currentPage ?? 0) + 1, totalPages ?? 0))
                }
              />
            </PaginationItem>
          </PaginationContent>
        </Pagination>
      )}
    </div>
  );
};

export default RecipeGrid;
