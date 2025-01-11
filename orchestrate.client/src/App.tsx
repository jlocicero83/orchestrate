import { ClerkProvider, useAuth } from '@clerk/clerk-react';
import { BrowserRouter, Route, Routes, Navigate } from "react-router-dom";
import Login from './pages/Login';

// This would be stored as env variable or secret
const PUBLISHABLE_KEY = import.meta.env.VITE_CLERK_PUBLISHABLE_KEY

if (!PUBLISHABLE_KEY) {
  throw new Error('Add your Clerk Publishable Key to the .env.local file')
}

function App() {
  return (
    <ClerkProvider publishableKey={PUBLISHABLE_KEY}>
      <BrowserRouter>
        <Routes>
          <Route path="/login" element={<Login />} />
          <Route 
            path="/" 
            element={
              <RequireAuth>
                HI!
              </RequireAuth>
            } 
          />
        </Routes>
      </BrowserRouter>
    </ClerkProvider>
  );
}

// Simple auth guard component
function RequireAuth({ children }: { children: React.ReactNode }) {
  const { isSignedIn, isLoaded } = useAuth();
  
  if (!isLoaded) {
    return <div>Loading...</div>;
  }
  
  if (!isSignedIn) {
    return <Navigate to="/login" replace />;
  }

  return <>{children}</>;
}

export default App;