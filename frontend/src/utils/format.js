
 export const formatearPrecio = (number) => {
    const formatter = new Intl.NumberFormat('en-US');
    let formatNumber = formatter.format(number);
    formatNumber = formatNumber.replace(/,/g, '.');
    return formatNumber;
  };