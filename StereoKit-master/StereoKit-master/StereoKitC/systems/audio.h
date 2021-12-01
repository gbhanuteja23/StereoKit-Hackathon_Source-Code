#pragma once

#include "../stereokit.h"

namespace sk {

#define AU_SAMPLE_RATE   48000
#define AU_SAMPLE_FORMAT ma_format_f32

struct _sound_inst_t {
	sound_t  sound;
	uint16_t id;
	vec3     position;
	float    volume;
};

bool audio_init     ();
void audio_update   ();
void audio_shutdown ();

extern _sound_inst_t au_active_sounds[8];

} // namespace sk