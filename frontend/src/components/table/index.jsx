import { useEffect, useState } from 'react';
import { RiArrowLeftSLine, RiArrowRightSLine, RiArrowUpSFill, RiArrowDownSFill } from '@remixicon/react';
import {
  flexRender,
  getCoreRowModel,
  getPaginationRowModel,
  useReactTable,
  getSortedRowModel,
  getFilteredRowModel
} from "@tanstack/react-table"
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeaderCell,
  TableRow,
  Button,
  TextInput,
} from "@tremor/react"
import { Link } from 'react-router-dom';
import { useNavigate } from 'react-router-dom';

function DataTable() {
  const navigate = useNavigate();
  const [products, setProducts] = useState([]);
  const [sorting, setSorting] = useState([]);
  const [filtering, setFiltering] = useState('');

  useEffect(() => {
    getProducts();
  }, []);

  const getProducts = async () => {
    try {
      const response = await fetch('/api/producto/');
      if (!response.ok) {
        throw new Error('Failed to fetch products');
      }
      const data = await response.json();
      setProducts(data);
    } catch (error) {
      console.error('Error al leer productos:', error);
    }
  };

  const columns = [
    {
      accessorKey: 'id',
      header: "Cód.",
    },
    {
      accessorKey: 'nombre',
      header: "Nombre Prod.",
    },
    {
      accessorKey: 'descripcion',
      header: "Descripción",
    },
    {
      accessorKey: 'marca',
      header: "Marca",
    },
    {
      accessorKey: 'proveedor',
      header: "Proveedor",
    },
    {
      accessorKey: 'cantidad',
      header: "Cant.",
    },
    {
      accessorKey: 'deposito',
      header: "Depósito",
    },
    {
      accessorKey: 'costo',
      header: "Costo",
    },
    {
      accessorKey: 'precio_mayorista',
      header: "Mayorista",
    },
    {
      accessorKey: 'precio_minorista',
      header: "Minorista",
    },
    {
      header: "Acciones",
    }
  ];

  const table = useReactTable({ 
    data: products, 
    columns, 
    getCoreRowModel: getCoreRowModel(), 
    getPaginationRowModel: getPaginationRowModel(), 
    getSortedRowModel: getSortedRowModel(),
    getFilteredRowModel: getFilteredRowModel(),
    state: {
      sorting,
      globalFilter: filtering
    },
    onSortingChange: setSorting,
    onGlobalFilterChange: setFiltering
  });

  return (
    <div>
      <TextInput className='max-w-sm' placeholder='Buscar por nombre, marca, proveedor, deposito, cantidad...' onChange={(e) => setFiltering(e.target.value)} />
      <Table className='my-5'>
        <TableHead>
          {table.getHeaderGroups().map((headerGroup) => (
            <TableRow key={headerGroup.id}>
              {headerGroup.headers.map((header) => (
                <TableHeaderCell className='p-2' key={header.id} onClick={header.column.getToggleSortingHandler()}>
                  {header.column.columnDef.header}
                  {[header.column.getIsSorted() ?? null]}
                </TableHeaderCell>
              ))}
            </TableRow>
          ))}
        </TableHead>
        <TableBody>
          {table.getRowModel().rows.map((row) => (
            <TableRow key={row.id} {...row.getRowProps()} >
              {row.getVisibleCells().map((cell) => (
                <TableCell className='p-2' key={cell.id}>
                  <Link to={`/productos/detalle/${row.original.id}`}>{flexRender(cell.column.columnDef.cell, cell.getContext())}</Link>
                </TableCell>
              ))}
            </TableRow>
          ))}
        </TableBody>
      </Table>
      <div className="flex justify-center space-x-5">
        <Button icon={RiArrowLeftSLine} iconPosition="left" variant="light" color='blue' onClick={() => table.previousPage()} disabled={!table.getCanPreviousPage()}>
          Anterior
        </Button>
        <p className="text-sm">Página  {table.getState().pagination.pageIndex + 1} de {table.getPageCount()} </p>
        <Button icon={RiArrowRightSLine} iconPosition="right" variant="light" color='blue' onClick={() => table.nextPage()} disabled={!table.getCanNextPage()}>
          Siguiente
        </Button>
      </div>
    </div>
  );
}

export default DataTable;
