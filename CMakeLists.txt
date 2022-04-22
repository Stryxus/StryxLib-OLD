# CMakeList.txt : Top-level CMake project file, do global configuration
# and include sub-projects here.
#
cmake_minimum_required (VERSION 3.12)

project ("StryxLib")

# Include sub-projects.
add_subdirectory ("StryxLib.C")
add_subdirectory ("StryxLib.CXX")
