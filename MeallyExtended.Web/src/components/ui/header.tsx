import { NavigateFunction, useNavigate } from "react-router-dom";
import { Button } from "./button";
import { useEffect, useState } from "react";
import apiBaseUrl from "../../../API/apiConfig";
import LoginForm from "./loginForm";
import RegisterForm from "./registerForm";
import axios, { AxiosResponse } from "axios";

interface HeaderProps {
  setIsLoggedIn?: (isLoggedIn: boolean) => void;
}

const Header: React.FC<HeaderProps> = (props) => {
  const nav: NavigateFunction = useNavigate();
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);

  useEffect(() => {
    checkLoggedIn();
  }, []);

  const checkLoggedIn = () => {
    axios
      .get(`${apiBaseUrl}/manage/info`, {
        withCredentials: true,
      })
      .then((response: AxiosResponse) => {
        if (response.status === 200) {
          setIsLoggedIn(true);
          if (props.setIsLoggedIn) {
            props.setIsLoggedIn(true);
          }
        } else {
          setIsLoggedIn(false);
          if (props.setIsLoggedIn) {
            props.setIsLoggedIn(false);
          }
        }
      })
      .catch(() => {
        setIsLoggedIn(false);
        if (props.setIsLoggedIn) {
          props.setIsLoggedIn(false);
        }
      });
  };

  const logout = () => {
    axios
      .post(
        `${apiBaseUrl}/api/User/logout`,
        {},
        {
          withCredentials: true,
        }
      )
      .then((response: AxiosResponse) => {
        if (response.status === 200) {
          setIsLoggedIn(false);
          if (props.setIsLoggedIn) {
            props.setIsLoggedIn(false);
          }
          nav("/");
        }
      })
      .catch(() => {
        console.error("An error occurred during logout");
      });
  };

  return (
    <header className="flex justify-between items-center px-4 py-2">
      <div>
        <a href="/" className="text-5xl font-bold">
          Meally
        </a>
        <p className="text-xs text-center">Your meal planner</p>
      </div>
      <div className="flex gap-2">
        {isLoggedIn ? (
          <>
            <Button onClick={() => nav("/create-recipe")}>
              Create New Recipe
            </Button>
            <Button variant="secondary" onClick={logout}>
              Logout
            </Button>
          </>
        ) : (
          <>
            <LoginForm setIsLoggedIn={setIsLoggedIn} />
            <RegisterForm />
          </>
        )}
      </div>
    </header>
  );
};

export default Header;
