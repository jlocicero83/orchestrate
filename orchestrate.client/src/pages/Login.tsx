import { useAuth0 } from "@auth0/auth0-react"
import { Navigate } from "react-router-dom"
import Logo from "../assets/OrchestrateLogo.svg"

function Login() {
  const { isAuthenticated, loginWithRedirect } = useAuth0()

  if (isAuthenticated) return <Navigate to="/dashboard" />

  return (
    <div className="min-h-screen flex flex-col items-center justify-center bg-[#121212]">
      <div>
        <img src={Logo} alt="OrchestrateLogo" />
      </div>
      <button onClick={() => loginWithRedirect()} className="mt-10 px-4 py-2 bg-blue-600 text-white rounded">
        Login with Auth0
      </button>
    </div>
  )
}

export default Login
