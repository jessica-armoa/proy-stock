'use client'

import React, { useEffect, useState } from "react";
import SidebarItem from "./SidebarItem";
import { useRouter } from 'next/navigation';

const Sidebar = () => {
  const router = useRouter();
  const [user, setUser] = useState(null);
  const [selectedItem, setSelectedItem] = useState('');

  useEffect(() => {
    const userData = localStorage.getItem("user");
    if (userData) {
      setUser(JSON.parse(userData));
    }
    const storedSelectedItem = localStorage.getItem('selectedItem');
    if (storedSelectedItem) {
      setSelectedItem(storedSelectedItem);
    }
  }, []);

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
        {menuItems.map((item) => (
          <SidebarItem key={item.path} item={item} selectedItem={selectedItem} setSelectedItem={setSelectedItem} />
        ))}
      </div>
      <div className="mt-auto">
        {user && (
          <div className="mb-3 p-3 rounded-lg bg-gray-100">
            <div className="font-bold">{user.userName}</div>
            <div className="text-sm text-gray-600">
              {user.role === 'Admin' ? user.role : `${user.role} ${user.deposito}`}
            </div>
          </div>
        )}
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
