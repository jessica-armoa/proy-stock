"use client";
import React from "react";


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
import { useState } from "react";

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

import { Link } from "react-router-dom";
import Filter from "../FilterFunction";

function DataTable({ columns, data }) {
  const [sorting, setSorting] = useState([]);
  const [filtering, setFiltering] = useState("");
  const [columnFilters, setColumnFilters] = React.useState([]);

  const headers = columns.slice(1, 4).map((column) => column.header); //Index 1 al 4, para saltear el index de codigo
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
      columnFilters: columnFilters,
    },
    onColumnFiltersChange: setColumnFilters,
    onSortingChange: setSorting,
    onGlobalFilterChange: setFiltering,
  });

  return (
    <div>
      <TextInput
        className="max-w-sm"
        placeholder={placeHolder}
        onChange={(e) => setFiltering(e.target.value)}
      ></TextInput>
      <Table className="my-5">
        <TableHead>
          {table.getHeaderGroups().map((headerGroup) => (
            <TableRow key={headerGroup.id}>
              {headerGroup.headers.map((header) => (
                <TableHeaderCell
                  className={("p-2 "+(columns[header.index].widthClass) ?? "")}
                  key={header.id}
                  onClick={header.column.getToggleSortingHandler()}
                >
                  <div>{header.column.columnDef.header}</div>
                  {
                    {
                      asc: (
                        <Button
                          className="remixicon size-2 color-primary bg-none border-none color-blue"
                          variant="secondary"
                          icon={RiArrowUpSFill}
                        />
                      ),
                      desc: (
                        <Button
                          className="remixicon size-2 color-primary bg-none border-none"
                          variant="secondary"
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
                <TableHeaderCell key={header.id} className={("p-2 "+(columns[header.index].widthClass) ?? "")}>
                  <div>
                    <Filter column={header.column} table={table} numericInputType={columns[header.index].numericInputType ?? 'single'} placeholder={"Filtrar "+columns[header.index].header} display={columns[header.index].search ?? true} inputClass={columns[header.index].inputClass ?? 'w-fit-content'}/>
                  </div>
                </TableHeaderCell>           
              ))}
            </TableRow>
          ))}
        </TableHead>

        <TableBody>
          {table.getRowModel().rows.map((row) => (
            <TableRow key={row.id} {...row.getRowProps}>
              {row.getVisibleCells().map((cell) => (
                <TableCell className="p-2 text-wrap">
                  <Link to={`/productos/detalle/${row.original.id}`}>
                    {flexRender(cell.column.columnDef.cell, cell.getContext())}
                  </Link>
                </TableCell>
              ))}
            </TableRow>
          ))}
        </TableBody>
      </Table>
      <div className="flex justify-center space-x-5">
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
