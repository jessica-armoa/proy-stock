'use client'

import React from 'react'
import SidebarItem from './items'
import { useNavigate } from 'react-router-dom';

const Sidebar = () => {
  const navigate = useNavigate();
  const menuItems = [
    { name: 'Dashboard',
    path: '/',
    icon: 'space_dashboard',
    subItems:[]},

    { name: 'Stock',
      path: '#',
      icon: 'storefront',
      subItems:[
        {
          name: 'Productos',
          path: '/productos',
          icon: 'package_2',
          isSubItem : true,
        },
        {
          name: 'Movimientos',
          path: '/movimientos',
          icon: 'compare_arrows',
          isSubItem : true,
        },
        {
          name: 'Depósitos',
          path: '/depositos',
          icon: 'package_2',
          isSubItem : true,
        }
      ]
    }
  ]

  const handleLogout = () => {
    localStorage.clear();
    navigate("/login");
  };

  return (
    <div className='mr-3 p-3 left-0 h-full rounded-lg bg-ui-sidebarbg z-1 w-sidebar flex flex-col'>
      <div className='p-3 mb-7'><img src='/img/logo.svg'></img></div>
      <div>
        {menuItems.map((item)=>{
          return <SidebarItem key={item.path} item={item} isSubItem={false}/>
        })
        }
      </div>
      <div className='p-3'>
        <button className='w-full bg-red-500 text-white p-2 rounded mt-500'
        onClick={handleLogout}>
          Cerrar sesión
        </button>
      </div>
    </div>

  )
}

export default Sidebar;