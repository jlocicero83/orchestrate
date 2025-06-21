// src/route/Private.tsx
import { useAuth0 } from '@auth0/auth0-react'
import { Navigate } from 'react-router-dom'

const ProtectedRoute = ({ children }: { children: JSX.Element }) => {
  const { isAuthenticated, isLoading } = useAuth0()

  if (isLoading) {
    return <div className="text-white text-center">Loading...</div>
  }

  return isAuthenticated ? children : <Navigate to="/" />
}

export default ProtectedRoute