import React from "react";

const ErrorPage: React.FC = () => {
  return (
    <div
      id="error-page"
      className="flex flex-col gap-8 justify-center items-center h-screen"
    >
      <h1 className="text-6xl font-bold"> {":("}</h1>
      <p className="text-xl text-slate-400">404</p>
      <p>The page you are trying to access could not be found.</p>
    </div>
  );
};

export default ErrorPage;
