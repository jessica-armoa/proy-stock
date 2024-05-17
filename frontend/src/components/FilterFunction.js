import React from "react";
import DebounceInput from "./DebounceFunction";

function Filter({ column, table }) {
  // Obtener el modelo de filas prefiltradas
  const preFilteredRows = table.getPreFilteredRowModel().rows;
  const firstRow = preFilteredRows[0];
  const firstValue = firstRow ? firstRow.getValue(column.id) : undefined;
  const columnFilterValue = column.getFilterValue();

  const sortedUniqueValues = React.useMemo(() => {
    // Ordenar valores únicos para mostrar en un datalist
    return typeof firstValue === "number"
      ? []
      : Array.from(column.getFacetedUniqueValues().keys()).sort();
  }, [column]);

  return typeof firstValue === "number" ? (
    <div>
      <div>
        <DebounceInput
          type="number"
          min={Number(column.getFacetedMinMaxValues()?.[0] ?? "")}
          max={Number(column.getFacetedMinMaxValues()?.[1] ?? "")}
          value={columnFilterValue?.[0] ?? ""}
          onChange={(value) =>
            column.setFilterValue([value, columnFilterValue?.[1]])
          }
          placeholder="min"
        />
        <DebounceInput
          type="number"
          min={Number(column.getFacetedMinMaxValues()?.[0] ?? "")}
          max={Number(column.getFacetedMinMaxValues()?.[1] ?? "")}
          value={columnFilterValue?.[1] ?? ""}
          onChange={(value) =>
            column.setFilterValue([columnFilterValue?.[0], value])
          }
          placeholder="max"
        />
      </div>
    </div>
  ) : (
    <>
      <datalist id={column.id + "list"}>
        {sortedUniqueValues.slice(0, 5000).map((value) => (
          <option value={value} key={value} />
        ))}
      </datalist>
      <DebounceInput
        type="text"
        value={columnFilterValue ?? ""}
        onChange={(value) => column.setFilterValue(value)}
        placeholder="Search"
        list={column.id + "list"}
      />
    </>
  );
}

export default Filter;
