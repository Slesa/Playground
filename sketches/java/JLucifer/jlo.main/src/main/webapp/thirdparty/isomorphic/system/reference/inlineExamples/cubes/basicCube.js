isc.CubeGrid.create({
	data: productData,
    width:"80%",
    hideEmptyFacetValues:true,
	
    formatCellValue:"isc.Format.toUSDollarString(value)",

	columnFacets:["quarter","month","metric"],
	rowFacets:["region","product"]
});



