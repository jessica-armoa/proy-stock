import React from "react";
import { ExportAsCsv } from "react-export-table";
import { Button } from "@tremor/react";

const ExportCSV = ({ data, whatToExport, title, fileName = "ReporteCSV" }) => {
  const headers = whatToExport
    .filter((key) => key.accessorKey)
    .map((key) => key.header);
  const accessorKeys = whatToExport
    .map((key) => key.accessorKey)
    .filter(Boolean);
  const filteredData = data.map((item) =>
    Object.fromEntries(
      Object.entries(item).filter(([key]) => accessorKeys.includes(key))
    )
  );
  return (
    <ExportAsCsv data={filteredData} headers={headers}>
      {(props) => <Button variant="light" color="green" className="mx-3" {...props}>Descargar CSV</Button>}
    </ExportAsCsv>
  );
};
export default ExportCSV;
