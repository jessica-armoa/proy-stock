"use client";
import React from "react";
import { Dialog, DialogPanel, Button } from "@tremor/react";
import { RiTruckLine } from "@remixicon/react";

const VistaNR = () => {
    const [isOpen, setIsOpen] = React.useState(false);
    return (
    <div>
        <Button icon={RiTruckLine} type="primary" color="blue" onClick={() => setIsOpen(true)}>
         Nota remisión
        </Button>
        <Dialog open={isOpen} onClose={(val) => setIsOpen(val)} static={true}>
          <DialogPanel className="text-right">
            <div className="text-left">
            <h3 className="text-lg text-tremor-content-strong mb-4">
              Nota de remisión
            </h3>
            <h1 className="text-lg font-semibold text-tremor-content-strong mb-3">
              Ferretería La llave maestra SRL
            </h1>
            <p>Comercio al por mayor y menor</p>
            <p>Calle Falsa 1234</p>
            <p></p>
            </div>
            <div className="text-left">
                <h5 >Timbrado N°: 123456789</h5>
                <p>Válido hasta: Mayo 2025</p>
                <h5>RUC: 123456789</h5>
                <h3 className="text-m font-semibold text-tremor-content-strong">NOTA DE REMISIÓN</h3>
                <h5>002-003-0000256</h5>

            </div>
            <p className="mt-2 leading-6 text-tremor-default text-tremor-content">
              Your account has been created successfully. You can now login to
              your account. For more information, please contact us.
            </p>
            <Button className="mt-8" type="primary" color="blue" onClick={() => setIsOpen(false)}>
            Cerrar
            </Button>
          </DialogPanel>
        </Dialog>
    </div>
  );
};

export default VistaNR;
