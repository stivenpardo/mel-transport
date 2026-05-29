import { createBrowserRouter } from "react-router-dom";
import Layout from "./Layout";
import Students from "./Students";

const router = createBrowserRouter([
  {
    path: "/",
    element: <Layout />,
    errorElement: <>Error Detail</>,
    children: [
      {
        index: true,
        element: <>Home</>,
      },
      {
        path: "/students",
        element: <Students />,
      },
    ],
  },
]);
export default router;
