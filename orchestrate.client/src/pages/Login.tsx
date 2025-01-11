import { SignIn } from "@clerk/clerk-react";

function Login() {
  return (
  <div className="min-h-screen flex items-center justify-center bg-gray-50">
    <div className="max-w-md w-full">
      <SignIn />
    </div>
  </div>
  );
};

export default Login;