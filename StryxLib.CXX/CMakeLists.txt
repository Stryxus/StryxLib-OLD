# CMakeList.txt : CMake project for StryxLib.C, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.12)
set(CMAKE_CXX_CLANG_TIDY clang-tidy -checks=-*,readability-*)

# Add source to this project's executable.
add_library (StryxLib.CXX SHARED "Sources/stdlib.cpp" "Headers/stdlib.hpp")

set_property(TARGET StryxLib.C PROPERTY CXX_STANDARD 23)

# TODO: Add tests and install targets if needed.
