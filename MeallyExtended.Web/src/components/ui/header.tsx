import { NavigateFunction, useNavigate } from "react-router-dom";
import { Button } from "./button";
import { useEffect, useState } from "react";
import apiBaseUrl from "../../../API/apiConfig";
import LoginForm from "./loginForm";
import RegisterForm from "./registerForm";
import axios, { AxiosResponse } from "axios";

interface HeaderProps {
  setIsLoggedIn?: (isLoggedIn: boolean) => void;
  setUserInfo?: (userInfo: string) => void;
}

const Header: React.FC<HeaderProps> = (props) => {
  const nav: NavigateFunction = useNavigate();
  const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);
  const [userInfo, setUserInfo] = useState<string>("");

  useEffect(() => {
    checkLoggedIn();
  }, []);

  useEffect(() => {
    if (props.setIsLoggedIn) {
      props.setIsLoggedIn(isLoggedIn);
    }
  }, [isLoggedIn]);

  useEffect(() => {
    if (props.setUserInfo) {
      props.setUserInfo(userInfo);
    }
  }, [userInfo]);

  const checkLoggedIn = () => {
    axios
      .get(`${apiBaseUrl}/manage/info`, {
        withCredentials: true,
      })
      .then((response: AxiosResponse) => {
        if (response.status === 200) {
          setIsLoggedIn(true);
          setUserInfo(response.data.email);
        } else {
          setIsLoggedIn(false);
        }
      })
      .catch(() => {
        setIsLoggedIn(false);
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
            <LoginForm setIsLoggedIn={setIsLoggedIn} setUserInfo={setUserInfo}/>
            <RegisterForm />
          </>
        )}
      </div>
    </header>
  );
};

export default Header;
