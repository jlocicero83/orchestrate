import { SignIn } from "@clerk/clerk-react";
import Logo from "../assets/OrchestrateLogo.svg";

function Login() {
  return (
  <div className="min-h-screen flex flex-col items-center justify-center bg-gradient-to-b from-PrimaryBlueDarkest from-75% to-PrimaryBlue">
    <div>
      <img src={Logo} alt="OrchestrateLogo" className="w-auto h-0/" />
    </div>
    <div className="max-w-md w-full pt-10 pl-6">
      <SignIn appearance={{
        variables: {
          colorPrimary: "#457B9D",
          colorBackground: "#F0F4FE",
          colorText: "#1D3557",
          fontFamily: "Arial, sans-serif",
        },
      }} />
    </div>
  </div>
  );
};

export default Login;