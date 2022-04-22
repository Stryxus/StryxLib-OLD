# CMakeList.txt : CMake project for StryxLib.C, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.12)

# Add source to this project's executable.
add_library (StryxLib.C SHARED "Sources/stdlib.c" "Headers/stdlib.h")

set_property(TARGET StryxLib.C PROPERTY C_STANDARD 23)

# TODO: Add tests and install targets if needed.
