#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 aTexCoord;
layout(location = 2) in vec4 aOffset;

out vec2 texCoord;
out float texIndex;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

void main(void)
{
  texCoord = aTexCoord;
  texIndex = aOffset.a;

  vec3 offset = vec3(aOffset.xyz);

  gl_Position = vec4(aPosition + offset, 1.0) * model * view * projection;
}

