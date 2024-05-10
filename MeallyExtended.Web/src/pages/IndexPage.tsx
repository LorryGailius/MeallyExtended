import { Button } from "@/components/ui/button";
import { useState } from "react";

// Each page is a React function component React.FC
const IndexPage: React.FC = () => {
  const [counter, setCounter] = useState(0);

  return (
    <>
      {/* Each page should start with parent div h-scren */}
      <div id="index-page" className="h-screen">
        <div className="flex flex-col gap-4 justify-center items-center">
          <h1 className="text-4xl font-bold">Meally</h1>
          <p className="text-lg">Your meal planner</p>
          <p className="text-lg">Counter: {counter}</p>
          <Button onClick={() => setCounter(counter + 1)}>
            This is shadcnUI button
          </Button>
        </div>
      </div>
    </>
  );
};

export default IndexPage;
