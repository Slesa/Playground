SPECS = creation
for(spec, SPECS) {
	exists($$spec) {
		SUBDIRS += $$spec
	}
}


