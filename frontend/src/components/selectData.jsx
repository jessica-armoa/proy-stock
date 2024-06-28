import React, { useState, useEffect } from 'react';
import { Select, SelectItem } from '@tremor/react';
import DepositosConfig from "../controladores/DepositosConfig";

// Componente que renderiza el selector de depósitos
export function SelectData({ setFilter }) {
    const [depositos, setDepositos] = useState([]);

    useEffect(() => {
        if (depositos.length <= 0) {
            DepositosConfig.getDepositos().then((response) => {
                setDepositos(response.data);
            });
        }
    }, []);

    const handleSelectChange = (event) => {
        const selectedValue = event.target.value;
        setFilter(selectedValue);
    };

    return (

        <div className="mx-auto max-w-xs">
            <Select defaultValue="Todos los depósitos" onChange={handleSelectChange}>
                <SelectItem value="">Todos los depósitos</SelectItem>
                {depositos.map((deposito) => (
                    <SelectItem key={deposito.id} value={deposito.str_nombre}>
                        {deposito.nombre}
                    </SelectItem>
                ))}
            </Select>
        </div>
    );
}
