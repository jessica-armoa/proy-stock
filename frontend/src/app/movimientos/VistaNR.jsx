'use client'
import dynamic from 'next/dynamic';
import React from "react";
import NotaRemisionPDF from "@/components/pdf/NotaRemisionPDF";
import { Dialog, DialogPanel, Button } from "@tremor/react";
import { RiTruckLine } from "@remixicon/react";

import { PDFViewer } from '@react-pdf/renderer';

//const NotaRemisionPDF = dynamic(() => import("@/components/pdf/NotaRemisionPDF"), { ssr: false });

const VistaNR = () => {
    const [isOpen, setIsOpen] = React.useState(false);
    return (
    <div>
        <Button icon={RiTruckLine} type="primary" color="blue" onClick={() => setIsOpen(true)}>
         Nota remisión
        </Button>
        <Dialog open={isOpen} onClose={(val) => setIsOpen(val)} static={true}>
          <DialogPanel className="text-right w-content">
            <div className="text-left">
              <h1 className="text-lg font-semibold text-tremor-content-strong mb-3">
                Nota de remisión
              </h1>
            </div>
            <PDFViewer className="w-pdf" key="pdfviewer"><NotaRemisionPDF/></PDFViewer>
            <Button className="mt-8" type="primary" color="blue" onClick={() => setIsOpen(false)}>
            Cerrar
            </Button>
          </DialogPanel>
        </Dialog>
    </div>
  );
};

export default VistaNR;
