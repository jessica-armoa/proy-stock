import React from "react";
import { ExportAsPdf } from "react-export-table";
import { Button } from "@tremor/react";

const ExportPDF = ({ data,whatToExport,title,fileName="ReportePDF",theme="striped"}) => {
  const headers = whatToExport.filter(key => key.accessorKey).map(key => key.header);
  const accessorKeys = whatToExport.map(key => key.accessorKey).filter(Boolean);
  const filteredData = data.map(item =>
    Object.fromEntries(
      Object.entries(item).filter(([key]) => accessorKeys.includes(key))
    )
  );
  return (
    <ExportAsPdf
      data={filteredData}
      headers={headers}
      headerStyles={{ fillColor: "#376FEA" }}
      title={title}
      fileName={fileName}
      theme={theme}
    >
      {(handleExport) => (
        <Button variant="light" color="violet" className="mx-3" {...handleExport}>Descargar PDF</Button>
      )}
    </ExportAsPdf>
  );
};
export default ExportPDF;
