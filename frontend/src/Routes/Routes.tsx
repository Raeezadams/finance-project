import { createBrowserRouter } from 'react-router-dom';
import App from '../App';
import HomePage from '../Pages/HomePage/HomePage';
import SearchPage from '../Pages/SearchPage/SearchPage';
import ComapnyPage from '../Pages/CompanyPage/ComapnyPage';
import CompanyProfile from '../Components/CompanyProfile/CompanyProfile';
import IncomeStatement from '../Components/IncomeStatement/IncomeStatement';
import DesignPage from '../Pages/DesignPage/DesignGuide';

export const router = createBrowserRouter([
  {
    path: '/',
    element: <App />,
    children: [
      { path: '', element: <HomePage /> },
      { path: 'search', element: <SearchPage /> },
      { path: "design-guide", element: <DesignPage/>},
      { path: 'company/:ticker', 
        element: <ComapnyPage />,
      children: [
        { path: 'company-profile', element: <CompanyProfile /> },
        { path: 'income-statement', element: <IncomeStatement /> },

      ] },
    ],
  },
]);
