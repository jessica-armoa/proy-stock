"use client";

import React from "react";
import SidebarItem from "./items";
import { useRouter } from 'next/navigation';

const Sidebar = () => {
  const router = useRouter();
  const menuItems = [
    { name: "Dashboard", path: "/", icon: "space_dashboard", subItems: [] },

    {
      name: "Stock",
      path: "#",
      icon: "storefront",
      subItems: [
        {
          name: "Productos",
          path: "/productos",
          icon: "package_2",
          isSubItem: true,
        },
        {
          name: "Movimientos",
          path: "/movimientos",
          icon: "compare_arrows",
          isSubItem: true,
        },
        {
          name: "Depósitos",
          path: "/depositos",
          icon: "package_2",
          isSubItem: true,
        },
      ],
    },
  ];

  const handleLogout = () => {
    localStorage.clear();
    router.push("/login");
  };

  return (
    <div className="mr-3 p-3 left-0 h-full rounded-lg bg-ui-sidebarbg z-1 w-sidebar flex flex-col">
      <div className="p-3 mb-7">
        <img src="/img/logo.svg" alt="Logo" />
      </div>
      <div>
        {menuItems.map((item) => {
          return <SidebarItem key={item.path} item={item} isSubItem={false} />;
        })}
      </div>
      <div className="mt-auto">
        <button
          className="flex items-center p-3 m-1 rounded-lg hover:bg-blue-100 cursor-pointer hover:text-ui-active justify-between"
          onClick={handleLogout}
        >
          <span className="material-symbols-outlined">logout</span>Cerrar sesión
        </button>
      </div>
    </div>
  );
};

export default Sidebar;
