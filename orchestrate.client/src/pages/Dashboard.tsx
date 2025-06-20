import { useAuth0 } from "@auth0/auth0-react"
import auth0Config from '../auth0-config';
import { useEffect, useState } from "react";

const baseUrl = import.meta.env.VITE_API_TARGET;
const Dashboard = ()=> {
  const { user, logout, getAccessTokenSilently, isAuthenticated, isLoading } = useAuth0()
  const [dashboardData, setDashboardData] = useState(null);
  console.log(baseUrl);
  const fetchDashboardInfo = async () => {
      try {
        const token = await getAccessTokenSilently();
        const url = baseUrl + "/api/dashboard/info"; // Use the target from vite.config.js
        const response = await fetch(url, {
          method: "GET",
          headers: {
            Authorization: `Bearer ${token}`
          }
        });

        const data = await response.json();
        setDashboardData(data);
        console.log(data); // { email, tenantId, userId }
      } catch (error) {
        console.error("Error fetching dashboard info", error);
      }
    };

  useEffect(() => {
    if (isAuthenticated) {
      fetchDashboardInfo();
    }
  }, [isAuthenticated]);

  if (isLoading) return <p>Loading...</p>;
  if (!isAuthenticated) return <p>Please log in.</p>;

  return (
    <div className="min-h-screen bg-gray-100 p-4">
      <div className="flex justify-between items-center mb-4">
        <h1 className="text-xl font-bold">Welcome, {user?.name}</h1>
        <p>Email (from Auth0): {user?.email}</p>
         {dashboardData ? (
        <>
          <p><strong>Tenant ID:</strong> {dashboardData.tenantId}</p>
          <p><strong>Tenant Name:</strong> {dashboardData.name}</p>
        </>
      ) : (
        <p>Loading tenant info...</p>
      )}

        <button
          onClick={() => logout({
            logoutParams: {
              returnTo: auth0Config.redirectUri,
            },
          })
        }
          className="bg-red-500 hover:bg-red-600 text-white px-4 py-2 rounded"
        >
          Logout
        </button>
      </div>
      <p className="text-gray-700">This is the dashboard page.</p>
    </div>
  )
}

export default Dashboard



