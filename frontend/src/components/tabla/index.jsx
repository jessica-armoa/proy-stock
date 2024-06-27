"use client";
import React, { useState, useEffect } from "react";
import ExportPDF from "../exportpdf";
import ExportCSV from "../exportcsv";

import {
  RiArrowLeftSLine,
  RiArrowRightSLine,
  RiArrowUpSFill,
  RiArrowDownSFill,
} from "@remixicon/react";

import {
  flexRender,
  getCoreRowModel,
  getPaginationRowModel,
  useReactTable,
  getSortedRowModel,
  getFilteredRowModel,
} from "@tanstack/react-table";

import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeaderCell,
  TableRow,
  Button,
  TextInput,
} from "@tremor/react";

import { useRouter } from 'next/navigation'
import Filter from "../FilterFunction";

function DataTable({ columns, data, pageurl, cantElementos=10 }) {
  const router = useRouter();
  const [sorting, setSorting] = useState([]);
  const [filtering, setFiltering] = useState("");
  const [columnFilters, setColumnFilters] = React.useState([]);

  const headers = columns.slice(1, 4).map((column) => column.header);
  const headerString = headers.join(", ");
  const placeHolder = "Buscar por " + headerString + ", etc.";

  const table = useReactTable({
    data,
    columns,
    getCoreRowModel: getCoreRowModel(),
    getPaginationRowModel: getPaginationRowModel(),
    getSortedRowModel: getSortedRowModel(),
    getFilteredRowModel: getFilteredRowModel(),
    state: {
      sorting,
      globalFilter: filtering,
      columnFilters,
    },
    onColumnFiltersChange: setColumnFilters,
    onSortingChange: setSorting,
    onGlobalFilterChange: setFiltering,
    initialState: {
      pagination: {
        pageSize: cantElementos,
      }
    }
  });

  const clearAllFilters = () => {
    setColumnFilters([]);
    setFiltering("");
  };

  const fechaRegex = /^(\d{4})-(\d{2})-(\d{2})T(\d{2}):(\d{2}):(\d{2})$/;

  const hasFilters = table.getState().columnFilters.length > 0 || table.getState().globalFilter;

  const filteredData = hasFilters ? table.getRowModel().rows.map(row => row.original) : data;

  const formatCurrency = (value) => {
    const formattedValue = Intl.NumberFormat('es-ES', {
      style: 'currency',
      currency: 'PYG',
      minimumFractionDigits: 0,
      useGrouping: true
    }).format(value);
    return formattedValue.replace('PYG', '');
  };

  const formatDate = (dateString) => {
    const options = {
        year: 'numeric',
        month: 'short',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit',
        hour12: false
    };

    const formattedDate = new Date(dateString).toLocaleString('es-ES', options);
    return formattedDate;
};
  
  const [hoveredRowId, setHoveredRowId] = useState(null);
  const [prevHoveredRowId, setPrevHoveredRowId] = useState(null);
  
  useEffect(() => {
    const btnActions = document.getElementById('btn-actions' + hoveredRowId?.id);
    if (btnActions) {
      if (prevHoveredRowId !== null && prevHoveredRowId !== hoveredRowId) {
        const prevBtnActions = document.getElementById('btn-actions' + prevHoveredRowId);
        if (prevBtnActions) {
          prevBtnActions.classList.add('invisible');
        }
      }
      if (hoveredRowId?.action === 'entering') {
        btnActions.classList.remove('invisible');
      } else if (hoveredRowId?.action === 'leaving') {
        btnActions.classList.add('invisible');
      }
    }
    setPrevHoveredRowId(hoveredRowId?.id);
  }, [hoveredRowId, prevHoveredRowId]);

  return (
    <div>
      <div className="flex justify-end mt-5">
        <Button onClick={clearAllFilters} variant="light" color="blue" className="mx-3">Limpiar Filtros</Button>
        <ExportPDF data={filteredData} whatToExport={columns} title={"Detalle de Stock"} fileName="reporte_stock_pdf"></ExportPDF>
        <ExportCSV data={filteredData} whatToExport={columns} fileName="reporte_stock_scv"></ExportCSV>
      </div>
      <Table>
        <TableHead>
          {table.getHeaderGroups().map((headerGroup) => (
            <TableRow key={headerGroup.id}>
              {headerGroup.headers.map((header) => (
                <TableHeaderCell
                  className={"p-0 m-0" + (columns[header.index].widthClass ?? "")}
                  key={header.id}
                  onClick={header.column.getToggleSortingHandler()}
                >
                  <div>{header.column.columnDef.header}</div>
                  {
                    {
                      asc: (
                        <Button
                          className="remixicon size-2 color-primary bg-none border-none color-blue"
                          variant="light"
                          icon={RiArrowUpSFill}
                        />
                      ),
                      desc: (
                        <Button
                          className="remixicon size-2 color-primary bg-none border-none"
                          variant="light"
                          icon={RiArrowDownSFill}
                        />
                      ),
                    }[header.column.getIsSorted() ?? null]
                  }
                </TableHeaderCell>
              ))}
            </TableRow>
          ))}

          {table.getHeaderGroups().map((headerGroup) => (
            <TableRow key={headerGroup.id}>
              {headerGroup.headers.map((header) => (
                <TableHeaderCell
                  key={header.id}
                  className={"p-2 " + (columns[header.index].widthClass ?? "")}
                >
                  <div>
                    <Filter
                      column={header.column}
                      table={table}
                      numericInputType={columns[header.index].numericInputType ?? "single"}
                      placeholder={"Filtrar " + columns[header.index].header}
                      display={columns[header.index].search ?? true}
                      inputClass={columns[header.index].inputClass ?? "w-fit-content"}
                    />
                  </div>
                </TableHeaderCell>
              ))}
            </TableRow>
          ))}
        </TableHead>

        <TableBody>
          {table.getRowModel().rows.map((row) => (
            <TableRow 
              key={row.id} {...row.getRowProps} className="clickable tablerow" 
              onClick={() => router.push(`${pageurl}${row.original.id}`)}
              onMouseEnter={() => setHoveredRowId({ id: row.original.id, action: 'entering' })}
              onMouseLeave={() => setHoveredRowId({ id: row.original.id, action: 'leaving' })}
            >
              {row.getVisibleCells().map((cell) => {
                let content;

                if (typeof cell.getValue() === 'number') {
                    content = formatCurrency(cell.getValue());
                } else if (fechaRegex.test(cell.getValue())) {
                    content = formatDate(cell.getValue());
                } else {
                    content = flexRender(cell.column.columnDef.cell, cell.getContext());
                }

                return (
                    <TableCell className="p-2 text-wrap" key={cell.id}>
                        <div className="truncate-y">{content}</div>
                    </TableCell>
                );
              })}
            </TableRow>
          ))}
        </TableBody>
      </Table>
      <div className="flex justify-center space-x-5 mt-5">
        <Button
          icon={RiArrowLeftSLine}
          iconPosition="left"
          variant="light"
          color="blue"
          onClick={() => table.previousPage()}
          disabled={!table.getCanPreviousPage()}
        >
          Anterior
        </Button>
        <p className="text-sm">
          Página {table.getState().pagination.pageIndex + 1} de{" "}
          {table.getPageCount()}{" "}
        </p>
        <Button
          icon={RiArrowRightSLine}
          iconPosition="right"
          variant="light"
          color="blue"
          onClick={() => table.nextPage()}
          disabled={!table.getCanNextPage()}
        >
          Siguiente
        </Button>
      </div>
    </div>
  );
}

export default DataTable;
