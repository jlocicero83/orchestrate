import { ClerkProvider } from '@clerk/clerk-react';
import { BrowserRouter, useRoutes } from "react-router-dom";
import { routes } from './routes';

// This would be stored as env variable or secret
const PUBLISHABLE_KEY = import.meta.env.VITE_CLERK_PUBLISHABLE_KEY

if (!PUBLISHABLE_KEY) {
  throw new Error('Add your Clerk Publishable Key to the .env.local file')
}

function AppRoutes() {
  return useRoutes(routes)
}

function App() {
  return (
    <ClerkProvider publishableKey={PUBLISHABLE_KEY}>
      <BrowserRouter>
        <AppRoutes />
      </BrowserRouter>
    </ClerkProvider>
  );
}

// // Simple auth guard component
// function RequireAuth({ children }: { children: React.ReactNode }) {
//   const { isSignedIn, isLoaded } = useAuth();
  
//   if (!isLoaded) {
//     return <div>Loading...</div>;
//   }
  
//   if (!isSignedIn) {
//     return <Navigate to="/login" replace />;
//   }

//   return <>{children}</>;
// }

export default App;