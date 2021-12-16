import Reading from "./pages/user/reading";
import Favourites from "./pages/user/favourites";
import Categories from "./pages/user/categories";
import Discover from "./pages/user/discover";
import Library from "./pages/admin/library";
import Users from "./pages/admin/users";

export const userRoutes = [
  { id: 0, name: "Discover", path: "/user/discover", element: Discover },
  {
    id: 1,
    name: "Categories",
    path: "/user/categories",
    element: Categories,
  },
  { id: 2, name: "Reading", path: "/user/reading", element: Reading },
  {
    id: 3,
    name: "Favourites",
    path: "/user/favourites",
    element: Favourites,
  },
];

export const adminRoutes = [
  {
    id: 0,
    name: "Library",
    path: "/admin/library",
    element: Library,
  },
  {
    id: 1,
    name: "Users",
    path: "/admin/users",
    element: Users,
  },
];
