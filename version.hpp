#pragma once

#include <inttypes.h>
#include <string>
#include "donate-level.hpp"

extern const char ver_long[];
extern const char ver_short[];
extern const char ver_html[];

inline std::string get_version_str()
{
	return std::string(ver_long) + std::to_string(uint32_t(fDevDonationLevel * 1000)) ;
}

inline std::string get_version_str_short()
{
	return std::string(ver_short);
}

//////////////////////////////////////////////////////////
//@AB
extern const char ver_messages[];
inline std::string get_version_messages()
{
	return std::string(ver_messages);
}
//////////////////////////////////////////////////////////