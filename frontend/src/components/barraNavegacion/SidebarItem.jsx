import { usePathname, useRouter } from 'next/navigation';
import React, { useMemo, useState, useEffect } from 'react';

const SidebarItem = ({ item }) => {
    const { name, icon, subItems, path, isSubItem } = item;
    const [expanded, setExpanded] = useState(false);
    const router = useRouter();
    const pathname = usePathname();

    useEffect(() => {
        const storedState = localStorage.getItem(`expanded_${name}`);
        if (storedState) {
            setExpanded(JSON.parse(storedState));
        }
    }, [name]);

    const onClick = () => {
        if (path !== '#') {
            router.push(item.path);
        }
        if (subItems && subItems.length > 0) {
            setExpanded((prev) => {
                const newState = !prev;
                localStorage.setItem(`expanded_${name}`, JSON.stringify(newState));
                return newState;
            });
        }
    };

    const isActive = useMemo(() => {
        return path === pathname;
    }, [path, pathname]);

    return (
        <>
            <div className={`flex item-center p-3 m-1 rounded-lg hover:bg-blue-100 cursor-pointer hover:text-ui-active justify-between ${isSubItem && "ml-3"} ${isActive && "text-ui-active bg-blue-100"}`}
                onClick={onClick}
            >
                <div className='flex space-x-2'>
                    <span className="material-symbols-outlined">{icon}</span>
                    <p>{name}</p>
                </div>
                {subItems && subItems.length > 0 && (
                    <span className={`material-symbols-outlined material-sm ${expanded ? "rotate-90" : ""}`}>chevron_right</span>
                )}
            </div>
            {expanded && subItems && subItems.length > 0 && (
                subItems.map((subItem) => {
                    return <SidebarItem key={subItem.path} item={subItem} />
                })
            )}
        </>
    );
};

export default SidebarItem;
