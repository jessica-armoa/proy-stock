import React from "react";
import DebounceInput from "./DebounceFunction";

function Filter({ column, table, numericInputType = "range", placeholder = "Search", display=true, widthClass="", inputClass="w-fit-content" }) {
  const preFilteredRows = table.getPreFilteredRowModel().rows;
  const firstRow = preFilteredRows[0];
  const firstValue = firstRow ? firstRow.getValue(column.id) : undefined;
  const columnFilterValue = column.getFilterValue();

  const sortedUniqueValues = React.useMemo(() => {
    return typeof firstValue === "number"
      ? []
      : Array.from(column.getFacetedUniqueValues().keys()).sort();
  }, [column]);

  //console.log(widthClass);
  // Configuración para inputs numéricos basada en el prop `numericInputType`
  const renderNumericInput = () => (
    <div className={"flex flex-wrap "}>
      {numericInputType === "range" ? (
        <>
          <DebounceInput
            className={inputClass}
            type="number"
            min={Number(column.getFacetedMinMaxValues()?.[0] ?? "")}
            max={Number(column.getFacetedMinMaxValues()?.[1] ?? "")}
            value={columnFilterValue?.[0] ?? ""}
            onChange={(value) => column.setFilterValue([value, columnFilterValue?.[1]])}
            placeholder="Min"
          />
          <DebounceInput
            className={inputClass}
            type="number"
            min={Number(column.getFacetedMinMaxValues()?.[0] ?? "")}
            max={Number(column.getFacetedMinMaxValues()?.[1] ?? "")}
            value={columnFilterValue?.[1] ?? ""}
            onChange={(value) => column.setFilterValue([columnFilterValue?.[0], value])}
            placeholder="Max"
          />
        </>
      ) : (
        <DebounceInput
          className={inputClass}
          type="number"
          value={columnFilterValue?.[0] ?? ""}
          onChange={(value) => column.setFilterValue([value,value])}
          placeholder={placeholder}
        />
      )}
    </div>
  );

  return display ? (typeof firstValue === "number" ? (
    renderNumericInput()
  ) : (
    <>
      <datalist id={column.id + "list"}>
        {sortedUniqueValues.slice(0, 5000).map((value) => (
          <option value={value} key={value} />
        ))}
      </datalist>
      <DebounceInput
        className={inputClass}
        type="text"
        value={columnFilterValue ?? ""}
        onChange={(value) => column.setFilterValue(value)}
        placeholder={placeholder}
        list={column.id + "list"}
      />
    </>
  )
) : <></>;
}

export default Filter;
